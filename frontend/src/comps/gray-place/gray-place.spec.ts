import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrayPlace } from './gray-place';

describe('GrayPlace', () => {
  let component: GrayPlace;
  let fixture: ComponentFixture<GrayPlace>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GrayPlace]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GrayPlace);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
