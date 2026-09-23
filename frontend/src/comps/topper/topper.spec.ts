import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Topper } from './topper';

describe('Topper', () => {
  let component: Topper;
  let fixture: ComponentFixture<Topper>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Topper]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Topper);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
