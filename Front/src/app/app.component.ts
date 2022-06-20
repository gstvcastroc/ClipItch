import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ModalComponent } from './modals/modal/modal.component';

const parent: string = 'localhost';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  selectedIndex: number = 0;

  closeResult: string | undefined;

  constructor(private http: HttpClient, private modalService: NgbModal) { }

  slides: Array<any> = [];

  next() {
    if (this.selectedIndex < (this.slides.length - 1)) {
      ++this.selectedIndex;
    }
  }

  previous() {
    if (this.selectedIndex > 0) {
      --this.selectedIndex;
    }
  }

  open(clipId: string) {
    const modalRef = this.modalService.open(ModalComponent,
      {
        size: 'lg',
        modalDialogClass: 'modal-dialog modal-dialog-centered'
      });

    let formatUrl: string = 'https://localhost:5001/api/v1/Clips/' + clipId;

    //use ClipId to get proper clip, need to implement te http request for this
    this.http.get<any>(formatUrl).subscribe(data => {
      modalRef.componentInstance.id = clipId;
      modalRef.componentInstance.url = 'https://clips.twitch.tv/embed?clip=' + data.id + '&autoplay=true&muted=false&parent=' + parent;
      modalRef.componentInstance.broadcaster_name = data.broadcaster_name;
      modalRef.componentInstance.title = data.title;
      modalRef.componentInstance.game_name = data.game_name;
      modalRef.componentInstance.profile_image_url = data.profile_image_url;
      modalRef.componentInstance.auth = false;
    })
  }

  allClips: any;

  backgroundImages: any = [
    { src: '../assets/images/apex.jpg', flag: '../assets/images/apexIcon.png', active: 'active', game_name: 'Apex Legends', color_overlay: '#7E342D', description: 'Apex Legends é um jogo eletrônico free-to-play do gênero battle royale desenvolvido pela Respawn Entertainment e publicado pela Electronic Arts. Foi lançado para Microsoft Windows, PlayStation 4 e Xbox One em fevereiro de 2019 e para Nintendo Switch em março de 2021.' },
    { src: '../assets/images/fortnite.jpg', flag: '../assets/images/fortniteIcon.png', active: '', game_name: 'Fortnite', color_overlay: '#427CA2', description: 'Fortnite é um jogo eletrônico multijogador online revelado originalmente em 2011, desenvolvido pela Epic Games e lançado como diferentes modos de jogo que compartilham a mesma jogabilidade e motor gráfico de jogo.' },
    { src: '../assets/images/overwatch.jpg', flag: '../assets/images/overwatchIcon.png', active: '', game_name: 'Overwatch', color_overlay: '#3C4667', description: 'Overwatch é um jogo eletrônico multijogador de tiro em primeira pessoa desenvolvido e publicado pela Blizzard Entertainment. Foi lançado em 24 de maio de 2016 para Microsoft Windows, PlayStation 4 e Xbox One e em 15 de outubro de 2019 para Nintendo Switch. ' }];

  ngOnInit() {
    this.http.get<any>('https://localhost:5001/api/v1/Games').subscribe(games => {
      this.http.get<any>('https://localhost:5001/api/v1/Clips/39').subscribe(clips => {
        this.allClips = clips;
        this.slides = games;
      })
    })
  }

  abrirModalAuth() {
    const modalRef = this.modalService.open(ModalComponent,
      {
        size: 'lg',
        modalDialogClass: 'modal-dialog modal-dialog-centered'
      });

    modalRef.componentInstance.auth = true;
  }

  notImplemented() {
    alert('Não implementado!')
  }
}
