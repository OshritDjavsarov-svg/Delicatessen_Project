import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ConectService } from '../../servises/conect-service';

@Component({
  selector: 'app-conect',
  imports: [FormsModule],
  templateUrl: './conect.html',
  styleUrl: './conect.css',
})
export class Conect {
  // --- הזרקות מודרניות ---
  private conectService = inject(ConectService);
  private router = inject(Router);

  // --- שימוש ב-Signals לניהול נתוני הטופס ---
  // נתוני התחברות
  loginData = {
    email: '',
    password: ''
  };

  // נתוני הרשמה (מבוסס על ה-Interface של User)
  registerData: any = {
    privateName: '',
    lastName: '',
    email: '',
    phone: '',
    city: '',
    addressStreet: '',
    houseNumber: null,
    buildEntry: null,
    apartment: null,
    buildFloor: null,
    password: '',
    passwordVerification: ''
  };

  // משתנה לניהול הצגת סיסמה
  isPasswordVisible = signal(false);

  // פונקציית התחברות
  onLogin() {
    this.conectService.login(this.loginData).subscribe({
      next: (user) => {
        alert(`ברוך שובך, ${user.privateName}!`);
        this.router.navigate(['/products']); // ניתוב לדף מוצרים
      },
      error: (err) => {
        console.error(err);
        alert('שגיאה: דוא"ל או סיסמה שגויים');
      }
    });
  }

  
  // פונקציית עזר להצגת סיסמה
  togglePassword() {
    this.isPasswordVisible.update(value => !value);
  }

  onRegister() {
    // 1. בדיקת התאמת סיסמאות (צד לקוח)
    if (this.registerData.password !== this.registerData.passwordVerification) {
      alert('הסיסמאות אינן תואמות!');
      return;
    }

    // 2. בניית האובייקט בפורמט PascalCase (כמו ב-#C)
    const userDto = {
      PrivateName: this.registerData.privateName,
      LastName: this.registerData.lastName,
      Email: this.registerData.email,
      Password: this.registerData.password,
      Phone: this.registerData.phone,
      City: this.registerData.city,
      AddressStreet: this.registerData.addressStreet, 
      HouseNumber: Number(this.registerData.houseNumber) || 0,
      BuildEntry: this.registerData.buildEntry ? String(this.registerData.buildEntry) : null,
      Apartment: this.registerData.apartment ? String(this.registerData.apartment) : null,
      BuildFloor: this.registerData.buildFloor ? String(this.registerData.buildFloor) : null,
      CreatedAt: new Date().toISOString()
    };

    console.log('נתונים שנשלחים לשרת:', userDto);

    // 3. שליחה לשרת
    this.conectService.register(userDto).subscribe({
      next: (user) => {
        alert('נרשמת בהצלחה!');
        this.router.navigate(['/products']);
      },
      error: (err) => {
        console.error('Full Error Detail:', err);
        
        // חילוץ הודעות השגיאה הספציפיות מה-Validation של השרת
        let validationErrors = '';
        if (err.error && err.error.errors) {
          // רץ על כל רשימת השגיאות שהשרת החזיר
          validationErrors = Object.values(err.error.errors).flat().join('\n');
        }

        alert(validationErrors || 'שגיאה ברישום. וודאי שכל שדות החובה מלאים והמייל תקין.');
      }
    });
  }
}
