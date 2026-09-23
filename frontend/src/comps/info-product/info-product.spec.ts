import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InfoProduct } from './info-product';

describe('InfoProduct', () => {
  let component: InfoProduct;
  let fixture: ComponentFixture<InfoProduct>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InfoProduct]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InfoProduct);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
