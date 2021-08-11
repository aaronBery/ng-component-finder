import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'app-villian',
    templateUrl: 'villian.component.html'
})

export class VillianComponent implements OnInit {
    @Input() title = 'Mwah mwah mwah mwah mwah';

    constructor() { }

    ngOnInit() { }
}