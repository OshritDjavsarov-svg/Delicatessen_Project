import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Product } from '../app/models/product';


@Injectable({
  providedIn: 'root',
})

export class ProductService {
  private http = inject(HttpClient);
  

  // סיגנל שיחזיק את רשימת המוצרים בכל האפליקציה
  products = signal<Product[]>([]);

  // משיכת מוצרים מהשרת
  getAllProducts(): Observable<Product[]> {
    const apiUrl = 'https://localhost:..../api/Products/getAllProductsCards';
    return this.http.get<Product[]>(apiUrl).pipe(
      tap(data => this.products.set(data)) // עדכון הסיגנל ברגע שהנתונים מגיעים
    );
  }

  getProductByNamefull(name: string): Observable<Product> {
    // שימוש ב-encodeURIComponent כדי לטפל ברווחים ועברית ב-URL
    const encodedName = encodeURIComponent(name);
    const url = `https://localhost:..../api/Products/GetProductByNamefull/${encodedName}`;
    
    return this.http.get<Product>(url);
  }
}
