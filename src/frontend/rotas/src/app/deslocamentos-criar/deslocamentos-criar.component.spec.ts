import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeslocamentosCriarComponent } from './deslocamentos-criar.component';

describe('DeslocamentosCriarComponent', () => {
  let component: DeslocamentosCriarComponent;
  let fixture: ComponentFixture<DeslocamentosCriarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DeslocamentosCriarComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeslocamentosCriarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
