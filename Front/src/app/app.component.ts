import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  constructor(private http: HttpClient) { }

  teste: any;

  ngOnInit() {

    this.http.get<any>('https://localhost:5001/api/v1/Games').subscribe(data => {
      this.teste = data;
    })    
  }
}
