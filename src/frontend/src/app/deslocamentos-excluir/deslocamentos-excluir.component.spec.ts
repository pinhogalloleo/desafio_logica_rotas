import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeslocamentosExcluirComponent } from './deslocamentos-excluir.component';

describe('DeslocamentosExcluirComponent', () => {
  let component: DeslocamentosExcluirComponent;
  let fixture: ComponentFixture<DeslocamentosExcluirComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeslocamentosExcluirComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeslocamentosExcluirComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
