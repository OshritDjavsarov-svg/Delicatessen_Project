import { Routes } from '@angular/router';
import { Conect } from '../comps/conect/conect';
import { Products } from '../comps/products/products';
import { InfoProduct } from '../comps/info-product/info-product';
import { CartComponent } from '../comps/cart/cart';


export const routes: Routes = [
  { path: 'conect', component: Conect },
  { path: 'products', component: Products },
  { path: 'product-info/:name', component: InfoProduct },
  { path: 'cart', component: CartComponent },
  { path: '', redirectTo: '/conect', pathMatch: 'full' }
];
