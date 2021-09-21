import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasetextboxComponent } from './basetextbox.component';

describe('BasetextboxComponent', () => {
  let component: BasetextboxComponent;
  let fixture: ComponentFixture<BasetextboxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasetextboxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasetextboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
