import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { ShopComponent } from './shop/shop.component';

//normal routing - all modules loaded at booting up of app
// const routes: Routes = [
//   { path: '', component: HomeComponent },
//   { path: 'shop', component: ShopComponent },
//   { path: 'shop/:id', component: ProductDetailsComponent },
//   { path: '**', redirectTo: '', pathMatch: 'full' }
// ];

//lazy loading modules - modules loaded when needed
const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule) },
  { path: '**', redirectTo: '', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
