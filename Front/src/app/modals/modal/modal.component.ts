import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {

  @Input() id!: string;
  @Input() url!: string;
  @Input() broadcaster_name!: string;
  @Input() title!: string;
  @Input() game_name!: string;
  @Input() profile_image_url!: string;
  @Input() auth: boolean = false;

  idUsuario: string = this.getCookie('user');
  rangeVal: string = '';

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  closeModal(sendData: any) {
    this.activeModal.close(sendData);
  }

  favoritarClipe(idClipe: string) {

    this.setCookie('userClipFav', idClipe, 1, 'localhost');

    var stringFormatada = `Clipe \"${idClipe}\" adicionado aos favoritos do usuário \"${this.idUsuario}\".`;

    console.log(stringFormatada);

    alert(stringFormatada);
  }

  avaliarClipe(idClipe: string, notaAvaliacao: string) {

    this.setCookie('userClipAval', idClipe, 1, 'localhost', notaAvaliacao);

    var stringFormatada = `O usuário \"${this.idUsuario}\" avaliou o clipe \"${idClipe}\" com  a nota:  ${notaAvaliacao}`;

    console.log(stringFormatada);

    alert(stringFormatada);
  }

  login(email : string, senha : string) {

    var idUser = Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);

    this.setCookie('user', idUser, 1, 'localhost');

    var getUser = this.getCookie('user');

    console.log(getUser);

    var stringFormatada = `Login de id: \"${this.idUsuario}\" feito com sucesso.`;

    alert(stringFormatada);
  }

  setCookie(name: string, value: string, expireDays: number, path: string = '', value1 : string = '') {
    let d: Date = new Date();
    d.setTime(d.getTime() + expireDays * 24 * 60 * 60 * 1000);
    let expires: string = `expires=${d.toUTCString()}`;
    let cpath: string = path ? `; path=${path}` : '';
    document.cookie = `${name}=${value};${value1}; ${expires};${cpath}`;
  }

  getCookie(name: string) {
    let ca: Array<string> = document.cookie.split(';');
    let caLen: number = ca.length;
    let cookieName = `${name}=`;
    let c: string;

    for (let i: number = 0; i < caLen; i += 1) {
      c = ca[i].replace(/^\s+/g, '');
      if (c.indexOf(cookieName) == 0) {
        return c.substring(cookieName.length, c.length);
      }
    }
    return '';
  }
}
