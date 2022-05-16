import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {

  @Input() url!: string;
  @Input() broadcaster_name!: string;
  @Input() title!: string;
  @Input() game_name!: string;
  @Input() creator_name!: string;

  constructor(public activeModal: NgbActiveModal) { }

  ngOnInit() {
  }

  closeModal(sendData: any) {
    this.activeModal.close(sendData);
  }
}
