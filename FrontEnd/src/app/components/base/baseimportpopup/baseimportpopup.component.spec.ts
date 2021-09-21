import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseimportpopupComponent } from './baseimportpopup.component';

describe('BaseimportpopupComponent', () => {
  let component: BaseimportpopupComponent;
  let fixture: ComponentFixture<BaseimportpopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BaseimportpopupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BaseimportpopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
