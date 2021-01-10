import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';

//normal routing - all modules loaded at booting up of app
// const routes: Routes = [
//   { path: '', component: HomeComponent },
//   { path: 'shop', component: ShopComponent },
//   { path: 'shop/:id', component: ProductDetailsComponent },
//   { path: '**', redirectTo: '', pathMatch: 'full' }
// ];

//lazy loading modules - modules loaded when needed
const routes: Routes = [
  { path: '', component: HomeComponent, data: {breadcrumb: 'Home'} },
  { path: 'test-error', component: TestErrorComponent, data: {breadcrumb: 'Test Error'} },
  { path: 'not-found', component: NotFoundComponent, data: {breadcrumb: 'Not Found'} },
  { path: 'server-error', component: ServerErrorComponent, data: {breadcrumb: 'Server Error'} },
  { path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule), data: {breadcrumb: 'Shop'} },
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
