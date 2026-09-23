import { HttpClient } from '@angular/common/http';
import { computed, inject, Injectable, signal } from '@angular/core';
import { ConectService } from './conect-service';

// ייבוא המודלים מהקובץ הנכון כפי שמופיע אצלך בפרויקט
import { Order } from '../app/models/cart';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private http = inject(HttpClient);
  private conectService = inject(ConectService);
  private apiUrl = 'https://localhost:..../api/Cart';

  // שימוש בטיפוס Order שהגדרת במודל
  cart = signal<Order | null>(null);

  totalItemsCount = computed(() => {
    const currentCart = this.cart();
    
    // בדיקה בטוחה: אם אין סל או אין פריטים, תחזיר 0
    const items = (currentCart as any)?.orderItems || (currentCart as any)?.items;
    
    if (!items || !Array.isArray(items)) {
      return 0;
    }

    return items.reduce((acc, item) => acc + item.quantity, 0);
  });

  // חישוב סכום כולל 
  totalPrice = computed(() => this.cart()?.totalPrice || 0);

  showSuccessNotification = signal(false);
  lastAddedProduct = signal<any>(null);

  /**
   * שליפת מזהה הלקוח מהסרוויס של ההתחברות
   */
  private getCustomerId(): number | null {
    const user = this.conectService.currentUser();
    console.log('User from service:', user)
    // בדיקה מול שדה id כפי שמופיע ב-Interface של ה-User שלך
    return user ? (user.customerId || (user as any).customerId) : null;
  }

  /**
   * טעינת הסל מהשרת לפי מזהה לקוח
   */
  loadCart(customerId: number) {
    this.http.get<Order>(`${this.apiUrl}/${customerId}`)
      .subscribe({
        next: (data) => this.cart.set(data),
        error: (err) => console.error('שגיאה בטעינת הסל:', err)
      });
  }

  /**
   * הוספת מוצר חדש או העלאת כמות פריט קיים
   */
  addToCart(product: any, quantity: number = 1) {
    const cId = this.getCustomerId();
    const pId = product.productId || product.ProductId;

    if (!cId || !pId) {
        console.error('חסרים נתונים לשליחה (לקוח או מוצר)');
        return;
    }

    // ה-Body נשלח עם אותיות גדולות כפי ש-C# מצפה ב-DTO
    const request = { 
        CustomerId: Number(cId), 
        ProductId: Number(pId), 
        Quantity: Number(quantity) 
    };

    this.http.post<Order>(`${this.apiUrl}/AddToCart`, request)
        .subscribe({
            next: (updatedCart) => {
              this.cart.set(updatedCart);
              this.lastAddedProduct.set(product); 
              this.showSuccessNotification.set(true);
              setTimeout(() => this.showSuccessNotification.set(false), 3000);
            },
            error: (err) => console.error('שגיאת שרת בהוספה לסל:', err)
        });
  }

  

  /**
   * מחיקת פריט לחלוטין מהסל
   */
  deleteItem(orderItemId: number) {
    const cId = this.getCustomerId();
    if (!orderItemId || !cId) return;

    this.http.delete<Order>(`${this.apiUrl}/deleteItem/${orderItemId}/${cId}`)
        .subscribe({
            next: (updatedCart) => this.cart.set(updatedCart),
            error: (err) => console.error('שגיאה במחיקת פריט:', err)
        });
  }

  /**
   * עדכון כמות עבור שורה קיימת בסל (מיועד ללחיצה על פלוס/מינוס)
   */
  updateItemQuantity(orderItemId: number, quantity: number) {
    const cId = this.getCustomerId();
    if (!cId) return;

    // בניית האובייקט בדיוק לפי ה-DTO בשרת
    const body = { 
        OrderItemId: orderItemId, 
        NewQuantity: quantity, 
        CustomerId: cId 
    };

    this.http.put<Order>(`${this.apiUrl}/updateItem`, body)
        .subscribe({
            next: (updatedCart) => {
                this.cart.set(updatedCart); // עדכון הסיגנל יעדכן את כל ה-UI
            },
            error: (err) => console.error('שגיאה בעדכון כמות:', err)
        });
  }


  placeOrder() {
    const cId = this.cart()?.customerId; // לקיחת ה-ID מהסל הנוכחי
    if (!cId) return;

    // שליחת ה-DTO כפי שהגדרת ב-C#
    return this.http.post<Order>(`${this.apiUrl}/place`, { customerId: cId }).pipe(
      tap((newOrder) => {
        this.cart.set(newOrder); // מעדכן את הסיגנל בסל החדש (הריק) שהשרת החזיר
      })
    );
  }

  

}