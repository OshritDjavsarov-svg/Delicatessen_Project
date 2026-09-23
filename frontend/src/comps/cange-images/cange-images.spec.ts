import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CangeImages } from './cange-images';

describe('CangeImages', () => {
  let component: CangeImages;
  let fixture: ComponentFixture<CangeImages>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CangeImages]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CangeImages);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
