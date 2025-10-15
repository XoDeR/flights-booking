import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookFlight } from './book-flight';

describe('BookFlight', () => {
  let component: BookFlight;
  let fixture: ComponentFixture<BookFlight>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookFlight]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookFlight);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
