import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'groupBy',
  standalone: true // מאפשר להשתמש בו ישירות בקומפוננטה
})
export class GroupByPipe implements PipeTransform {
  // הפונקציה שמבצעת את הקסם
  transform(collection: any[], property: string): any[] {
    if (!collection) return [];

    //  מקבצים את המוצרים לפי הקטגוריה שלהם
    const groupedCollection = collection.reduce((previous, current) => {
      if (!previous[current[property]]) {
        previous[current[property]] = [];
      }
      previous[current[property]].push(current);
      return previous;
    }, {});

    // הפיכת האובייקט למערך שנוכל לרוץ עליו ב-HTML עם @for
    return Object.keys(groupedCollection).map(key => ({
      category: key,
      items: groupedCollection[key]
    }));
  }
}