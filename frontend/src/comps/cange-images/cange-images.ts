import { CommonModule } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-cange-images',
  imports: [CommonModule],
  templateUrl: './cange-images.html',
  styleUrl: './cange-images.css',
})
export class CangeImages implements OnInit, OnDestroy {
  // מערך התמונות
  images = [
    { src: "images/disign/foto/bread.PNG", category: "לחמים" },
    { src: "images/disign/foto/cake.PNG", category: "עוגות" },
    { src: "images/disign/foto/chocolats.PNG", category: "שוקולד מבית R2M" },
    { src: "images/disign/foto/cookies.PNG", category: "עוגיות" },
    { src: "images/disign/foto/desserts.PNG", category: "קינוחים" },
    { src: "images/disign/foto/frozen.PNG", category: "בייקרי קפוא" },
    { src: "images/disign/foto/patisar.PNG", category: "מאפים" },
    { src: "images/disign/foto/salt.PNG", category: "מוצרים מלוחים" }
  ];

  currentIndex: number = 0;
  intervalId: any;

  ngOnInit() {
    this.startImageSlider();
  }

  startImageSlider() {
    this.intervalId = setInterval(() => {
      // הוספת המודולו (%) גורמת לכך שכשנגיע ל-8, זה יחזור אוטומטית ל-0
      this.currentIndex = (this.currentIndex + 1) % this.images.length;
    }, 3000);
  }

  // חשוב מאוד: ניקוי הטיימר כשהקומפוננטה נהרסת כדי למנוע זליגת זיכרון
  ngOnDestroy() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
    }
  }
}
