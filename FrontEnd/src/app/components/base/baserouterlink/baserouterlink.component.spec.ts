import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaserouterlinkComponent } from './baserouterlink.component';

describe('BaserouterlinkComponent', () => {
  let component: BaserouterlinkComponent;
  let fixture: ComponentFixture<BaserouterlinkComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BaserouterlinkComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BaserouterlinkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
