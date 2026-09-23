import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Conect } from './conect';

describe('Conect', () => {
  let component: Conect;
  let fixture: ComponentFixture<Conect>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Conect]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Conect);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
