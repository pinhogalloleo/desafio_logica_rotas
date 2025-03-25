import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeslocamentosListarComponent } from './deslocamentos-listar.component';

describe('DeslocamentosListarComponent', () => {
  let component: DeslocamentosListarComponent;
  let fixture: ComponentFixture<DeslocamentosListarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeslocamentosListarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeslocamentosListarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
