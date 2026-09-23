import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LastFooter } from './last-footer';

describe('LastFooter', () => {
  let component: LastFooter;
  let fixture: ComponentFixture<LastFooter>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LastFooter]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LastFooter);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
