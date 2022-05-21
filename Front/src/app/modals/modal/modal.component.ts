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

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  closeModal(sendData: any) {
    this.activeModal.close(sendData);
  }

  favoritarClipe(idClipe : string) {
    /*ToDo: adicionar em uma lista do usuario os clipes favoritados e no menu para ver quais foram favoritados listar todos.*/
  }

  avaliarClipe(idClipe: string) {
    /*ToDo: adicionar avaliação no db.*/
  }
}
