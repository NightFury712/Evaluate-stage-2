import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseradiobtnComponent } from './baseradiobtn.component';

describe('BaseradiobtnComponent', () => {
  let component: BaseradiobtnComponent;
  let fixture: ComponentFixture<BaseradiobtnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BaseradiobtnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseradiobtnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
