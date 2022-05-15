import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from './modals/modal/modal.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  selectedIndex: number = 0;

  closeResult: string | undefined;

  constructor(private http: HttpClient, private modalService: NgbModal) { }

  slides: any;

  next() {
    ++this.selectedIndex;
  }

  previous() {
    if (this.selectedIndex > 0) {
      --this.selectedIndex;
    }
  }

  open(clipId: number) {
    const modalRef = this.modalService.open(ModalComponent,
      {
        scrollable: true,
        windowClass: 'modalClass'
      });

    this.http.get<any>('https://localhost:5001/api/v1/Clips/1').subscribe(data => {
      modalRef.componentInstance.fromParent = data[0];
    })
  }

  dailyClips: any;

  weeklyClips: any;

  allClips: any;

  listaMenus: any = [{ url: '/', name: 'Home' }, { url: '/', name: 'Login' }];

  backgroundImages: any = [
    { src: '../assets/images/apex.jpg', flag: '../assets/images/apexIcon.png', active: 'active', game_name: 'Apex Legends', color_overlay: '#7E342D', description: 'Mussum Ipsum, cacilds vidis litro abertis. Quem manda na minha terra sou euzis!Si num tem leite então bota uma pinga aí cumpadi!Copo furadis é disculpa de bebadis, arcu quam euismod magna.Interagi no mé, cursus quis, vehicula ac nisi.' },
    { src: '../assets/images/fortnite.jpg', flag: '../assets/images/fortniteIcon.png', active: '', game_name: 'Fortnite', color_overlay: '#427CA2', description: 'Mussum Ipsum, cacilds vidis litro abertis. Quem manda na minha terra sou euzis!Si num tem leite então bota uma pinga aí cumpadi!Copo furadis é disculpa de bebadis, arcu quam euismod magna.Interagi no mé, cursus quis, vehicula ac nisi.' },
    { src: '../assets/images/overwatch.jpg', flag: '../assets/images/overwatchIcon.png', active: '', game_name: 'Overwatch', color_overlay: '#3C4667', description: 'Mussum Ipsum, cacilds vidis litro abertis. Quem manda na minha terra sou euzis!Si num tem leite então bota uma pinga aí cumpadi!Copo furadis é disculpa de bebadis, arcu quam euismod magna.Interagi no mé, cursus quis, vehicula ac nisi.' }];

  ngOnInit() {
    this.http.get<any>('https://localhost:5001/api/v1/Games').subscribe(games => {
      this.http.get<any>('https://localhost:5001/api/v1/Clips/daily/12').subscribe(daily => {
        this.http.get<any>('https://localhost:5001/api/v1/Clips/weekly/12').subscribe(weekly => {
          this.dailyClips = daily;
          this.weeklyClips = weekly;
          this.slides = games;
        })
      })
    })
  }
}
