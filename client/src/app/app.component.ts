import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IProduct} from "./models/product";
import {IPagination} from "./models/pagination";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'SkiNet';
  products: IProduct[] = [];

  constructor(private http: HttpClient) {

  }

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/products').subscribe((response: any) => {
      this.products = response.data;
    }, error => {
      console.log(error);
    })
  }


}
