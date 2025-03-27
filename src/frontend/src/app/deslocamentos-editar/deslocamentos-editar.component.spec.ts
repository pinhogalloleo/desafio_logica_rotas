import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeslocamentosEditarComponent } from './deslocamentos-editar.component';

describe('DeslocamentosEditarComponent', () => {
  let component: DeslocamentosEditarComponent;
  let fixture: ComponentFixture<DeslocamentosEditarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeslocamentosEditarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeslocamentosEditarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
