import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Projeto Medicamento';

  menuShow() {
    const ul: any = document.querySelector<HTMLUListElement>('nav ul');
    const menuBtn = document.querySelector<HTMLElement>('.menu-btn i');
    if (ul.classList.contains('open')) {
      ul.classList.remove('open');
    } else {
      ul.classList.add('open');
    }
  }
}
