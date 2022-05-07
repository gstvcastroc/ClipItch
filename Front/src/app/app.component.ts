import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  constructor(private http: HttpClient) { }

  listaGames: any;

  weeklyClips: any;

  allClips: any;

  listaMenus: any = [{ url: '/', name: 'Home' }, { url: '/', name: 'Login' }];

  backgroundImages: any = [
    { src: '../assets/images/apex.jpg', active: 'active', game_name: 'Apex Legends', description: 'Mussum Ipsum, cacilds vidis litro abertis. Quem manda na minha terra sou euzis!Si num tem leite então bota uma pinga aí cumpadi!Copo furadis é disculpa de bebadis, arcu quam euismod magna.Interagi no mé, cursus quis, vehicula ac nisi.' },
    { src: '../assets/images/fortnite.jpg', active: '', game_name: 'Fortnite', description: 'Mussum Ipsum, cacilds vidis litro abertis. Quem manda na minha terra sou euzis!Si num tem leite então bota uma pinga aí cumpadi!Copo furadis é disculpa de bebadis, arcu quam euismod magna.Interagi no mé, cursus quis, vehicula ac nisi.'  },
    { src: '../assets/images/overwatch.jpg', active: '', game_name: 'Overwatch', description: 'Mussum Ipsum, cacilds vidis litro abertis. Quem manda na minha terra sou euzis!Si num tem leite então bota uma pinga aí cumpadi!Copo furadis é disculpa de bebadis, arcu quam euismod magna.Interagi no mé, cursus quis, vehicula ac nisi.'  }];

  ngOnInit() {
    this.http.get<any>('https://localhost:5001/api/v1/Clips/weekly/10').subscribe(clipes => {
      this.weeklyClips = clipes;
    })
  }
}
