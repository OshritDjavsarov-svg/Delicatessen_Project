import { Injectable, PLATFORM_ID, inject, signal, computed } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { User } from '../app/models/user';

@Injectable({
  providedIn: 'root',
})
export class ConectService {
  // --- הזרקות מודרניות ---
  private http = inject(HttpClient);
  private platformId = inject(PLATFORM_ID);

  private apiUrl = 'https://localhost:..../api/Users';

  // --- ניהול מצב עם Signals ---
  // המשתנה הפרטי שמחזיק את המידע
  private currentUserSignal = signal<User | null>(null);

  // חשיפת המידע לקריאה בלבד עבור הקומפוננטות
  //חשיפת המידע לקריאה בלבד. זה מבטיח שרק ה-סרוויס רשאי לבצע שינויים
  public currentUser = this.currentUserSignal.asReadonly(); 
  
  // יצירת משתנה עזר שאומר האם המשתמש מחובר (מתעדכן אוטומטית)
  public isLoggedIn = computed(() => this.currentUserSignal() !== null);

  constructor() {
    if (isPlatformBrowser(this.platformId)) {
      const savedUser = localStorage.getItem('selected_user');
      if (savedUser) {
        try {
          this.currentUserSignal.set(JSON.parse(savedUser));
        } catch (e) {
          console.error('Error parsing user from localStorage', e);
        }
      }
    }
  }


  login(credentials: any): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/login`, credentials).pipe(
      tap(user => this.saveUser(user))
    );
  }

  register(userData: any): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/register`, userData).pipe(
      tap(user => this.saveUser(user))
    );
  }

  logout(): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('selected_user');
    }
    this.currentUserSignal.set(null);
  }

  private saveUser(user: User): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem('selected_user', JSON.stringify(user));
    }
    this.currentUserSignal.set(user);
  }
}