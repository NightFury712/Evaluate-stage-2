import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagedevelopingComponent } from './pagedeveloping.component';

describe('PagedevelopingComponent', () => {
  let component: PagedevelopingComponent;
  let fixture: ComponentFixture<PagedevelopingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PagedevelopingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PagedevelopingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
