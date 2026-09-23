import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FirstFooter } from './first-footer';

describe('FirstFooter', () => {
  let component: FirstFooter;
  let fixture: ComponentFixture<FirstFooter>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FirstFooter]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FirstFooter);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
