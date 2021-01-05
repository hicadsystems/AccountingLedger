<template>
    <!-- WRAPPER -->
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm" action="/NpfContribution/createNPfContribution" method="post">
            <div class="card-body">
                <div class="row">
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">fund type</label>
                            <select class="form-control" v-model="postBody.fundtype" name="fundtype" required :readonly="submitorUpdate == 'Update'">
                                <option v-for="balSheet in balanceSheetList" v-bind:value="balSheet.code" v-bind:key="balSheet.code"> {{ balSheet.description }} </option>
                            </select>
                        </div>
                    </div>
                    <!--<div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Loan type</label>
                            <select class="form-control" v-model="postBody.loantype" name="loantype">
                                <option v-for="balSheet in bankList" v-bind:value="balSheet.code" v-bind:key="balSheet.code"> {{ balSheet.description }} </option>
                            </select>
                        </div>
                    </div>-->
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">OS</label>
                            <input class="form-control" name="amount01" v-model="postBody.amount01" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">SM</label>
                            <input class="form-control" name="amount02" v-model="postBody.amount02" placeholder="" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">AB</label>
                            <input type="text" name="amount03" class="form-control" v-model="postBody.amount03" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">LS</label>
                            <input class="form-control" name="amount04" v-model="postBody.amount04" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">PO</label>
                            <input class="form-control" name="amount05" v-model="postBody.amount05" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">WO</label>
                            <input class="form-control" name="amount06" v-model="postBody.amount06" placeholder="" />
                        </div>
                    </div>

                </div>

                <div class="row">
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">MWO</label>
                            <input type="text" name="amount07" class="form-control" v-model="postBody.amount07" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">NWO</label>
                            <input class="form-control" name="amount08" v-model="postBody.amount08" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">S/Lt</label>
                            <input class="form-control" name="amount09" v-model="postBody.amount09" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Lt</label>
                            <input class="form-control" name="amount10" v-model="postBody.amount10" placeholder="" />
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Lt/Cdr</label>
                            <input type="text" name="amount11" class="form-control" v-model="postBody.amount11" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Cdr</label>
                            <input class="form-control" name="amount12" v-model="postBody.amount12" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Capt</label>
                            <input class="form-control" name="amount13" v-model="postBody.amount13" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Cdre</label>
                            <input class="form-control" name="amount14" v-model="postBody.amount14" placeholder="" />
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">R/Adm</label>
                            <input type="text" name="amount15" class="form-control" v-model="postBody.amount15" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">V/Adm</label>
                            <input class="form-control" name="amount16" v-model="postBody.amount16" placeholder="" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Adm</label>
                            <input class="form-control" name="amount17" v-model="postBody.amount17" placeholder="Period" />
                        </div>
                    </div>
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Contract</label>
                            <input class="form-control" name="amount18" v-model="postBody.amount18" placeholder="" />
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-12 col-xl-3">
                        <div class="form-group">
                            <label class="form-label">Retired</label>
                            <input type="text" name="amount19" class="form-control" v-model="postBody.amount19" placeholder="" />
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group">
                        <button class="btn btn-submit btn-primary" v-on:click="checkForm" type="submit">{{submitorUpdate}}</button>
                        </div>
                    </div>
                </div>
            </div>
            
        </form>
    </div>

    <!-- END WRAPPER -->
</template>

<script>
export default {

    data() {
        return {
            errors: null,
            responseMessage:'',
            submitorUpdate : 'Submit',
            canProcess: true,
            balanceSheetList: null,
            bankList:null,
            postBody: {
                fundtype: '',
                loantype: '',
                amount01: 0,
                amount02: 0,
                amount03: 0,
                amount04: 0,
                amount05: 0,
                amount06: 0,
                amount07: 0,
                amount08: 0,
                amount09: 0,
                amount10: 0,
                amount11: 0,
                amount12: 0,
                amount13: 0,
                amount14: 0,
                amount15: 0,
                amount16: 0,
                amount17: 0,
                amount18: 0,
                amount19: 0,
                id:0
            }

        }
        },
      mounted () {
        axios
            .get('/api/NpfContribution/getAllFundType')
              .then(response => (this.balanceSheetList = response.data)),
              axios
                    .get('/api/NpfContribution/getAllLoanType')
                    .then(response => (this.bankList = response.data))
     },

    watch:{
        '$store.state.objectToUpdate': function (FundCode, Rate, Period, Intrest) {
                 this.postBody.fundtype = this.$store.state.objectToUpdate.fundtype,
                 //this.postBody.loantype = this.$store.state.objectToUpdate.loantype,
                 this.postBody.amount01 = this.$store.state.objectToUpdate.amount01,
                 this.postBody.amount02 = this.$store.state.objectToUpdate.amount02,
                 this.postBody.amount03 = this.$store.state.objectToUpdate.amount03,
                 this.postBody.amount04 = this.$store.state.objectToUpdate.amount04,
                 this.postBody.amount05 = this.$store.state.objectToUpdate.amount05,
                 this.postBody.amount06 = this.$store.state.objectToUpdate.amount06,
                 this.postBody.amount07 = this.$store.state.objectToUpdate.amount07,
                 this.postBody.amount08 = this.$store.state.objectToUpdate.amount08,
                 this.postBody.amount09 = this.$store.state.objectToUpdate.amount09,
                 this.postBody.amount10 = this.$store.state.objectToUpdate.amount10,
                 this.postBody.amount11 = this.$store.state.objectToUpdate.amount11,
                 this.postBody.amount12 = this.$store.state.objectToUpdate.amount12,
                 this.postBody.amount13 = this.$store.state.objectToUpdate.amount13,
                 this.postBody.amount14 = this.$store.state.objectToUpdate.amount14,
                 this.postBody.amount15 = this.$store.state.objectToUpdate.amount15,
                 this.postBody.amount16 = this.$store.state.objectToUpdate.amount16,
                 this.postBody.amount17 = this.$store.state.objectToUpdate.amount17,
                this.postBody.amount18 = this.$store.state.objectToUpdate.amount18,
                this.postBody.amount19 = this.$store.state.objectToUpdate.amount19,
                this.postBody.id = this.$store.state.objectToUpdate.id
                this.submitorUpdate = 'Update';

        }
    },
    methods: {
        checkForm: function (e) {

            if (this.postBody.fundtype) {
              e.preventDefault();
              this.canProcess = false;
              this.postPost();
          }
          else{

          this.errors = [];
            this.errors.push('fundcode  or loantype is Required');
          }
        },
        postPost() {

                if(this.submitorUpdate == 'Submit'){
                    axios.post(`/api/NpfContribution/createNPfContribution`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if (response.data.responseCode == '200') {
                                this.postbody.fundtype= '';
                               // this.postbody.loantype= '';
                                this.postbody.amount01 = '';
                                this.postbody.amount02= '';
                                this.postbody.amount03= '';
                                this.postbody.amount04 = '';
                                this.postbody.amount05 = '';
                                    this.postbody.amount06 = '';
                                    this.postbody.amount07 = '';
                                    this.postbody.amount08 = '';
                                    this.postbody.amount09 = '';
                                    this.postbody.amount10 = '';
                                    this.postbody.amount11 = '';
                                    this.postbody.amount12 = '';
                                    this.postbody.amount13 = '';
                                    this.postbody.amount14 = '';
                                    this.postbody.amount15 = '';
                                    this.postbody.amount16 = '';
                                    this.postbody.amount17 = '';
                                    this.postbody.amount18 = '';
                                    this.postbody.amount19 = '';
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            if (this.submitorUpdate == 'Update') {
                    axios.put(`/api/NpfContribution/updateNPFContribution`, this.postBody )
                        .then(response => {
                            this.responseMessage = response.data.responseDescription;this.canProcess = true;
                            if(response.data.responseCode == '200'){
                                this.submitorUpdate = 'Submit';

                                    this.postbody.fundtype= '';
                                 //   this.postbody.loantype= '';
                                    this.postbody.amount01 = '';
                                    this.postbody.amount02= '';
                                    this.postbody.amount03= '';
                                    this.postbody.amount04 = '';
                                    this.postbody.amount05 = '';
                                    this.postbody.amount06 = '';
                                    this.postbody.amount07 = '';
                                    this.postbody.amount08 = '';
                                    this.postbody.amount09 = '';
                                    this.postbody.amount10 = '';
                                    this.postbody.amount11 = '';
                                    this.postbody.amount12 = '';
                                    this.postbody.amount13 = '';
                                    this.postbody.amount14 = '';
                                    this.postbody.amount15 = '';
                                    this.postbody.amount16 = '';
                                    this.postbody.amount17 = '';
                                    this.postbody.amount18 = '';
                                    this.postbody.amount19 = '';
                                 
                            }
                        })
                        .catch(e => {
                            this.errors.push(e)
                        });
                }
            }
        },
        computed: {
            setter(){
                let objecttoedit = this.$store.state.objectToUpdate;
                if (objecttoedit.fundtype) {
                    this.postBody.fundtype = objecttoedit.fundtype;
                   // this.postBody.loantype = objecttoedit.loantype;
                    this.postBody.amount01 = objecttoedit.amount01;
                     this.postBody.amount02 = objecttoedit.amount02;
                     this.postBody.amount03 = objecttoedit.amount03;
                     this.postBody.amount04 = objecttoedit.amount04;
                     this.postBody.amount05 = objecttoedit.amount05;
                     this.postBody.amount06 = objecttoedit.amount06;
                     this.postBody.amount07 = objecttoedit.amount07;
                     this.postBody.amount08 = objecttoedit.amount08;
                     this.postBody.amount09 = objecttoedit.amount09;
                     this.postBody.amount10 = objecttoedit.amount10;
                     this.postBody.amount11 = objecttoedit.amount11;
                     this.postBody.amount12 = objecttoedit.amount12;
                     this.postBody.amount13 = objecttoedit.amount13;
                     this.postBody.amount14 = objecttoedit.amount14;
                     this.postBody.amount15 = objecttoedit.amount15;
                     this.postBody.amount16 = objecttoedit.amount16;
                    this.postBody.amount17 = objecttoedit.amount17;
                    this.postBody.amount18 = objecttoedit.amount18;
                    this.postBody.amount19 = objecttoedit.amount19;
                   
                }
            }
        }
};
</script>