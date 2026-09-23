import { Component, inject } from '@angular/core';
import { CartService } from '../../servises/cart-service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-add-success-component',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './add-success-component.html',
  styleUrl: './add-success-component.css',
})
export class AddSuccessComponent {
  public cartService = inject(CartService);
}
