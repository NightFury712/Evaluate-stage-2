import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseexportpopupComponent } from './baseexportpopup.component';

describe('BaseexportpopupComponent', () => {
  let component: BaseexportpopupComponent;
  let fixture: ComponentFixture<BaseexportpopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BaseexportpopupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseexportpopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
