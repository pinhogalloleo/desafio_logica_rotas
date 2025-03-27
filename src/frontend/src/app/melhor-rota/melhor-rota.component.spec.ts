import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MelhorRotaComponent } from './melhor-rota.component';

describe('MelhorRotaComponent', () => {
  let component: MelhorRotaComponent;
  let fixture: ComponentFixture<MelhorRotaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MelhorRotaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MelhorRotaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
