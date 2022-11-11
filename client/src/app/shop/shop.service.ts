import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {IPagination} from "../models/pagination";

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = "https://localhost:5001/api/";

  constructor(private http: HttpClient) { }

  getProducts(){
      return this.http.get<IPagination>(this.baseUrl+ 'products');
  }
}
