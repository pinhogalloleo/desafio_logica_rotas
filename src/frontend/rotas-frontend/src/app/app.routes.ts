import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'viagens',
        loadComponent: () =>
            import('./features/viagens/list/list.component').then((m) => m.ListComponent),
    },
    {
        path: 'melhor-rota',
        loadComponent: () =>
            import('./features/melhor-rota/calculate/calculate.component').then((m) => m.CalculateComponent),
    },
    {
        path: '',
        redirectTo: '/viagens',
        pathMatch: 'full'
    }
];
