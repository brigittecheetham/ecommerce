import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { BreadcrumbService } from 'xng-breadcrumb';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity: number = 1;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, private breadcrumbService: BreadcrumbService, private basketService: BasketService) {
    this.breadcrumbService.set('@productDetails', ' ');
    console.log(this.breadcrumbService);
   }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    var productId = parseInt(this.activatedRoute.snapshot.paramMap.get('id'));
    this.shopService.getProduct(productId).subscribe(product => {
      this.product = product;
      this.breadcrumbService.set('@productDetails', product.name);
    }, error => {
      console.log(error);
    });
  }

  incrementQuantity() {
      this.quantity++;
  }

  decrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.product, this.quantity);
  }
}


