import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasetableComponent } from './basetable.component';

describe('BasetableComponent', () => {
  let component: BasetableComponent;
  let fixture: ComponentFixture<BasetableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasetableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasetableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
