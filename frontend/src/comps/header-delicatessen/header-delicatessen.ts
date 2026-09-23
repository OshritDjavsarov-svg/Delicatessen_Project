import { CommonModule } from '@angular/common';
import { Component, effect, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ConectService } from '../../servises/conect-service';
import { CartService } from '../../servises/cart-service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-header-delicatessen',
  imports: [CommonModule, RouterLink],
  templateUrl:'./header-delicatessen.html',
  styleUrl: './header-delicatessen.css',
})
export class HeaderDelicatessen implements OnInit {
  public conectService = inject(ConectService);
  public cartService = inject(CartService);
  private location = inject(Location);

  constructor() {
    // effect - פועל בכל פעם שהמשתמש משתנה
    effect(() => {
      const user = this.conectService.currentUser();
      if (user) {
        const cId = user.customerId || (user as any).id;
        // טעינת הסל מיד כשיש משתמש, בלי לחכות לביקור בדף הסל
        this.cartService.loadCart(cId);
      }
    });
  }

  ngOnInit() {
    // אפשר להשאיר ריק אם משתמשים ב-effect למעלה
  }

  // פונקציית החזרה אחורה
  back() {
    this.location.back();
  }
  
}
