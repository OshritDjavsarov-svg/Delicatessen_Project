import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderDelicatessen } from '../comps/header-delicatessen/header-delicatessen';
import { Topper } from '../comps/topper/topper';
import { FirstFooter } from '../comps/first-footer/first-footer';
import { LastFooter } from '../comps/last-footer/last-footer';
import { AddSuccessComponent } from '../comps/add-success-component/add-success-component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,
            HeaderDelicatessen,
            Topper,
            FirstFooter,
            LastFooter,
            AddSuccessComponent],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('DelicatessenProjectUser');
}
