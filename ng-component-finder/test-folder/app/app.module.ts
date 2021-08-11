import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HeroComponent } from './hero.component';

export const routes: Routes = [
    {
        path: '',
    },
    {
        path: 'hero',
        component: HeroComponent,
    }

];

@NgModule({
    imports: [
        RouterModule.forRoot(routes, {}),
    ],
})
export class AppModule { }