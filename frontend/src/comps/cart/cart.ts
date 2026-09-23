import { Component, OnInit, inject, computed, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { CartService } from '../../servises/cart-service';
import { ConectService } from '../../servises/conect-service';
import { ProductService } from '../../servises/product-service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterLink,FormsModule],
  templateUrl: './cart.html',
  styleUrl: './cart.css'
})
export class CartComponent implements OnInit {
  public cartService = inject(CartService);
  private conectService = inject(ConectService);
  public productService = inject(ProductService); // הזרקת סרוויס המוצרים
  public router = inject(Router);

  isCouponValid = signal<boolean>(false);
  // סיגנל שיחזיק את אחוז ההנחה (למשל 0.1 עבור 10%)
  discountPercent = signal<number>(0); 
  deliveryPrice = 60;
  couponCode = ''; 

  // חישוב סכום ההנחה בשקלים (לצורך תצוגה)
  discountAmount = computed(() => {
    const total = this.cartService.cart()?.totalPrice || 0;
    return total * this.discountPercent();
  });

  // חישוב הסכום הסופי לתשלום
  finalAmount = computed(() => {
    const total = this.cartService.cart()?.totalPrice || 0;
    const delivery = this.deliveryPrice;
    const discount = this.discountAmount();
    
    const result = (total + delivery) - discount;
    return result > 0 ? result : 0;
  });

  applyCoupon() {
    if (this.couponCode === 'אושרית') {
      this.discountPercent.set(0.10); // מעדכן ל-10 אחוז הנחה
      this.isCouponValid.set(true);
      alert('קופון 10% הוחל בהצלחה!');
    } else {
      this.discountPercent.set(0);
      this.isCouponValid.set(false);
      alert('קוד קופון לא תקין');
      this.couponCode = "";
    }
  }

  ngOnInit() {
    // טעינת הסל במידה והוא לא טעון
    const user = this.conectService.currentUser();
    if (user) {
      const cId = user.customerId || (user as any).id;
      this.cartService.loadCart(cId);
    }
  }

  // עדכון כמות (פלוס/מינוס)
  updateQty(item: any, amount: number) {
    const newQty = item.quantity + amount;
    
    if (newQty <= 0) {
      this.cartService.deleteItem(item.orderItemId);
    } else if (newQty <= 5) {
      this.cartService.updateItemQuantity(item.orderItemId, newQty);
    }
    
  }

  // ניקוי עגלה
  clearCart() {
    if (confirm('האם אתה בטוח שברצונך לנקות את העגלה?')) {
      const items = this.getItems();
      items.forEach((item: any) => {
        if (item.orderItemId) {
          this.cartService.deleteItem(item.orderItemId);
        }
      });
    }
  }

  // פונקציית עזר לשליפת הפריטים בצורה בטוחה
  getItems(): any[] {
    const cart = this.cartService.cart();
    if (!cart) return [];

    const items = (cart as any).orderItems || (cart as any).items || [];
    
    // מיון המערך לפי ID כדי שהסדר לא ישתנה לעולם בתצוגה
    return [...items].sort((a, b) => a.productId - b.productId);
  }

  // פונקציית עזר למציאת קטגוריה לפי ID מוצר
  getCategoryPath(productId: number): string {
    const allProducts = this.productService.products();
    const foundProduct = allProducts.find(p => p.productId === productId);
    
    // אם נמצא מוצר, נחזיר את שם הקטגוריה שלו, אחרת ברירת מחדל
    return foundProduct ? foundProduct.categoryName : 'לחמים';
  }

  // בניית נתיב התמונה המלא
  getItemImage(item: any): string {
    const category = this.getCategoryPath(item.productId);
    return `images/${category}/${item.productId-1}.png`;
  }

  onCheckout() {
    const finalPrice = this.finalAmount();
    
    this.cartService.placeOrder()?.subscribe({
      next: () => {
        // ה-alert עוצר את הקוד עד שהמשתמש לוחץ אישור
        alert(`ההזמנה בוצעה בהצלחה! סכום סופי: ${finalPrice.toFixed(2)} ₪`);
        
        // 3. הניתוב קורה מיד אחרי הלחיצה על אישור ב-alert
        this.router.navigate(['/products']); 
      },
      error: (err) => {
        console.error('Order failed:', err);
        alert('חלה שגיאה בביצוע ההזמנה.');
      }
    });
  }

  
}