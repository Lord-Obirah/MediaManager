import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppSingleSelectComponent } from './app-single-select.component';

describe('AppSingleselectComponent', () => {
  let component: AppSingleSelectComponent;
  let fixture: ComponentFixture<AppSingleSelectComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AppSingleSelectComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AppSingleSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
