import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderDelicatessen } from './header-delicatessen';

describe('HeaderDelicatessen', () => {
  let component: HeaderDelicatessen;
  let fixture: ComponentFixture<HeaderDelicatessen>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HeaderDelicatessen]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HeaderDelicatessen);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
