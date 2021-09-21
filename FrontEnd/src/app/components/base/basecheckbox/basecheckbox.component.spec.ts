import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasecheckboxComponent } from './basecheckbox.component';

describe('BasecheckboxComponent', () => {
  let component: BasecheckboxComponent;
  let fixture: ComponentFixture<BasecheckboxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasecheckboxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasecheckboxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
