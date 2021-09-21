import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasepopupinfoComponent } from './basepopupinfo.component';

describe('BasepopupinfoComponent', () => {
  let component: BasepopupinfoComponent;
  let fixture: ComponentFixture<BasepopupinfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasepopupinfoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasepopupinfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
