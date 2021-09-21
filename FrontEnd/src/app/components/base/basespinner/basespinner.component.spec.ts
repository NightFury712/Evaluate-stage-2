import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasespinnerComponent } from './basespinner.component';

describe('BasespinnerComponent', () => {
  let component: BasespinnerComponent;
  let fixture: ComponentFixture<BasespinnerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasespinnerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasespinnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
