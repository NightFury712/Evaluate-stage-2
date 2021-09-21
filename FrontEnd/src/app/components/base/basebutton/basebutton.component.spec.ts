import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasebuttonComponent } from './basebutton.component';

describe('BasebuttonComponent', () => {
  let component: BasebuttonComponent;
  let fixture: ComponentFixture<BasebuttonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasebuttonComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasebuttonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
