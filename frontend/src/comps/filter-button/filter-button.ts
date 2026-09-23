import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filter-button',
  standalone: true,
  imports: [FormsModule], // דרוש עבור [(ngModel)]
  templateUrl: './filter-button.html',
  styleUrl: './filter-button.css',
})
export class FilterButton {
  // משתנים לשליטה על פתיחת התפריטים
  isOtherDropdownOpen = false;
  isCategoryDropdownOpen = false;

  // רשימת האפשרויות לסינון (Checkboxes)
  filterOptions = [
    { label: 'טבעוני', value: 'טבעוני', checked: false },
    { label: 'ללא לקטוז', value: 'ללא לקטוז', checked: false },
    { label: 'ילדים אוהבים', value: 'ילדים אוהבים', checked: false },
    { label: 'ללא קמח חיטה', value: 'ללא קמח חיטה', checked: false },
    { label: 'צמחוני', value: 'צמחוני', checked: false }
  ];

  // רשימת הקטגוריות
  categories = [
    'עוגות', 'קינוחים', 'מאפים', 'לחמים', 
    'מוצרים מלוחים', 'עוגיות', 'שוקולד מבית R2M', 'בייקרי קפוא', 'הכל'
  ];

  // אירועים שנשלחים לקומפוננטת האב (המוצרים)
  @Output() categoryChanged = new EventEmitter<string>();
  @Output() filtersChanged = new EventEmitter<string[]>();


  // פתיחת/סגירת תפריט אפשרויות סינון
  toggleOtherFilter() {
    this.isOtherDropdownOpen = !this.isOtherDropdownOpen;
    this.isCategoryDropdownOpen = false;
  }

  // פתיחת/סגירת תפריט קטגוריות
  toggleCategoryFilter() {
    this.isCategoryDropdownOpen = !this.isCategoryDropdownOpen;
    this.isOtherDropdownOpen = false;
  }

  // לחיצה על קטגוריה
  onCategorySelect(category: string) {
    this.categoryChanged.emit(category);
    this.isCategoryDropdownOpen = false; // סגירת התפריט לאחר בחירה
  }

  // לחיצה על אישור בסינון רכיבים
  applyFilters() {
    const selectedValues = this.filterOptions
      .filter(opt => opt.checked)
      .map(opt => opt.value);
    
    this.filtersChanged.emit(selectedValues);
    this.isOtherDropdownOpen = false;
  }
}