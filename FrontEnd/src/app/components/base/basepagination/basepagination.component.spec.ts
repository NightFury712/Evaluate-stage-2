import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BasepaginationComponent } from './basepagination.component';

describe('BasepaginationComponent', () => {
  let component: BasepaginationComponent;
  let fixture: ComponentFixture<BasepaginationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BasepaginationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BasepaginationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
