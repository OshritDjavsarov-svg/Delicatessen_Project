import { Component, inject, OnInit, signal } from '@angular/core';
import { ProductService } from '../../servises/product-service';
import { CommonModule } from '@angular/common';
import { GroupByPipe } from '../../pipes/group-by.pipe';
import { CangeImages } from '../cange-images/cange-images';
import { GrayPlace } from '../gray-place/gray-place';
import { FilterButton } from '../filter-button/filter-button';
import { Product } from '../../app/models/product';
import { RouterLink } from '@angular/router';
import { CartService } from '../../servises/cart-service';

@Component({
  selector: 'app-products',
  imports: [CommonModule,
            GroupByPipe,
            CangeImages,
            GrayPlace,
            FilterButton,
            RouterLink],
  standalone: true,
  templateUrl: './products.html',
  styleUrl: './products.css'
  
})
export class Products implements OnInit {
  public productService = inject(ProductService);
  
  allProducts: Product[] = [];      // מערך שיכיל את המוצרים האמיתיים
  filteredProducts = signal<Product[]>([]); // מה שמוצג למשתמש
  
  currentCategory: string = 'הכל';
  activeFilters: string[] = [];

  ngOnInit() {
    // 1. הגדרת ערכי ברירת מחדל (כבר עשית את זה בהצהרה של המשתנים)
    this.currentCategory = 'הכל';
    this.activeFilters = [];

    // 2. קבלת הנתונים מהשרת
    this.productService.getAllProducts().subscribe((data: any[]) => {
      this.allProducts = data; // שומר את המקור
      
      // 3. הפעלת הסינון באופן יזום
      // מכיוון ש-currentCategory היא "הכל", זה פשוט ימלא את filteredProducts בכל המוצרים
      this.applyAllFilters(); 
      
    });
  }

  // פונקציה שמופעלת כשנבחרת קטגוריה
  onCategoryChange(category: string) {
    console.log("קטגוריה שנבחרה:", category);
    this.currentCategory = category;
    this.applyAllFilters();
  }

  // פונקציה שמופעלת כשלוחצים על "אישור" באפשרויות סינון
  onFiltersChange(filters: string[]) {
    this.activeFilters = filters;
    this.applyAllFilters();
  }
  

  applyAllFilters() {
    const result = this.allProducts.filter(product => {
      
      // 1. הכנת נתוני המוצר לבדיקה (ניקוי רווחים ושמות שדות)
      const pCategory = (product.categoryName || (product as any).categoryName || "").toString().trim();
      // אנחנו מניחים שהפילטרים במוצר עשויים להיות מופרדים בפסיקים או רווחים
      const pFilter = (product.filterProduct || (product as any).filterProduct || "").toString().trim();

      // 2. סינון לפי קטגוריה (נשאר אותו דבר - "הכל" או התאמה מדויקת)
      const categoryMatch = this.currentCategory === 'הכל' || pCategory === this.currentCategory.trim();

      // 3. סינון לפי מאפיינים (לוגיקת AND - "גם וגם")
      // activeFilters הוא מערך של מה שהמשתמש סימן (למשל: ['טבעוני', 'ללא לקטוז'])
      const filterMatch = this.activeFilters.every(selectedFilter => {
        // בדיקה האם המאפיין הספציפי קיים בתוך השדה של המוצר
        return pFilter.includes(selectedFilter.trim());
      });

      return categoryMatch && filterMatch;
    });

    // console.log('קטגוריה:', this.currentCategory);
    // console.log('פילטרים שנבחרו:', this.activeFilters);
    // console.log('תוצאה:', this.filteredProducts.length);
    
    this.filteredProducts.set(result)
  }


  private cartService = inject(CartService);

  onAddToCart(product: any) {
    console.log('Sending to service:', product);
    // קוראים לפונקציה בסרוויס ששולחת POST לשרת
    this.cartService.addToCart(product);
  }
}

