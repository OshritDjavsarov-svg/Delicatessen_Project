
import { Component, OnInit, effect, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Product } from '../../app/models/product';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../servises/product-service';
import { CartService } from '../../servises/cart-service';
import { ConectService } from '../../servises/conect-service';
@Component({
  selector: 'app-info-product',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './info-product.html',
  styleUrl: './info-product.css'
})
export class InfoProduct implements OnInit {
  private route = inject(ActivatedRoute);
  private productService = inject(ProductService);
  private conectService = inject(ConectService);
  public cartService = inject(CartService);

  currentProduct: any; // המוצר שמוצג כרגע בדף
  
  product = signal<Product | null>(null);
  isLoading = signal<boolean>(true);
  qty = signal<number>(0); // 0 אומר שהמוצר לא בסל

  // מצבי תצוגה (במקום לשנות DOM, נשנה בוליאנים)
  showHeating = signal<boolean>(false);
  showDetails = signal<boolean>(false);


  constructor() {
    /**
     * Effect: מאזין לשינויים בסיגנלים באופן אוטומטי.
     * ברגע שהסל בסרוויס מתעדכן (למשל אחרי loadCart), 
     * הפונקציה הזו תרוץ שוב ותעדכן את ה-qty שמוצג על המסך.
     */
    effect(() => {
      const currentCart = this.cartService.cart();
      const productData = this.product();

      if (productData && currentCart) {
        // שליפת רשימת הפריטים (תומך בשמות שדות שונים מה-API)
        const items = (currentCart as any).orderItems || (currentCart as any).items || [];
        
        // חיפוש המוצר הנוכחי בתוך הסל
        const itemInCart = items.find((i: any) => 
          (i.productId || (i as any).ProductId) === productData.productId
        );
      }
    });
  }


  ngOnInit() {
    // 1. שליפת שם המוצר מהכתובת (URL)
    const productName = this.route.snapshot.paramMap.get('name');

    // 2. הבטחת טעינת הסל - אם המשתמש מחובר, נטען את הסל שלו מיד
    // זה מבטיח שברגע שנרצה להוריד כמות, ה-orderItemId כבר יהיה קיים בזיכרון
    const user = this.conectService.currentUser();
    if (user) {
      const cId = user.customerId || (user as any).id;
      this.cartService.loadCart(cId);
    }

    if (productName) {
      // 3. שליפת פרטי המוצר מהשרת
      this.productService.getProductByNamefull(productName).subscribe({
        next: (data: any) => {
          this.product.set(data);
          
          // 4. סנכרון כמות: בדיקה אם המוצר הזה כבר נמצא בסל של המשתמש
          // אנחנו משתמשים ב-setTimeout קצר או בבדיקה ישירה כדי לוודא שהסל נטען
          this.syncProductQuantity();
          
          this.isLoading.set(false);
        },
        error: (err) => {
          console.error('שגיאה בטעינת המוצר:', err);
          this.isLoading.set(false);
        }
      });
    }
  }

  // פונקציית עזר פנימית לסנכרון הכמות (כדי לשמור על קוד נקי)
  private syncProductQuantity() {
    const currentCart = this.cartService.cart();
    const productData = this.product();

    if (currentCart && productData) {
      // גמישות בשמות שדות: בודק גם orderItems וגם items
      const cartItems = (currentCart as any).orderItems || (currentCart as any).items || [];
      
      const itemInCart = cartItems.find((i: any) => 
        (i.productId || (i as any).ProductId) === productData.productId
      );

      if (itemInCart) {
        this.qty.set(itemInCart.quantity);
      } else {
        this.qty.set(0);
      }
    }
  }


  toggleHeating() {
    this.showHeating.update(v => !v);
  }

  toggleDetails() {
    this.showDetails.update(v => !v);
  }



  changeQty(amount: number) {
    const currentProduct = this.product();
    if (!currentProduct) return;

    const currentQty = this.qty();
    const newQty = currentQty + amount;

    // 1. הגנות כמות
    if (newQty > 5 || newQty < 0) return;

    // 2. עדכון UI מיידי (בשביל חווית משתמש מהירה)
    this.qty.set(newQty);

    // 3. שליפה "טרייה" של הפריט מהסל בכל לחיצה
    const currentCart = this.cartService.cart();
    const items = (currentCart as any)?.orderItems || (currentCart as any)?.items || [];
    
    const cartItem = items.find((i: any) => 
      (i.productId || (i as any).ProductId) === currentProduct.productId
    );

    // 4. לוגיקת ביצוע
    if (amount > 0) {
      // פלוס: תמיד addToCart
      this.cartService.addToCart(currentProduct, 1);
    } 
    else if (amount < 0) {
      // מינוס: חייבים לוודא שיש cartItem בשביל ה-ID
      if (cartItem) {
        if (newQty === 0) {
          this.cartService.deleteItem(cartItem.orderItemId);
        } else {
          this.cartService.updateItemQuantity(cartItem.orderItemId, newQty);
        }
      } else {
        // מקרה קצה: אם לחצנו מינוס והוא לא מצא את הפריט
        // נבצע טעינה מהירה של הסל כדי "לרענן" את הזיכרון
        const cId = this.conectService.currentUser()?.customerId;
        if (cId) this.cartService.loadCart(cId);
      }
    }
  }


}