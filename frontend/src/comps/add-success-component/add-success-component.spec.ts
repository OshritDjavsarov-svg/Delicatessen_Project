import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSuccessComponent } from './add-success-component';

describe('AddSuccessComponent', () => {
  let component: AddSuccessComponent;
  let fixture: ComponentFixture<AddSuccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddSuccessComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddSuccessComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
